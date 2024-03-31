import { Component, OnInit, ViewChild } from '@angular/core';
import { Chart, ChartConfiguration, ChartItem, ChartTypeRegistry, registerables } from 'chart.js'
import { HttpClient } from '@angular/common/http';
import { School } from './models/school';

import { ReportsService } from '../reports.service'
import { SchoolAverage } from './models/schoolAverage';
import { Observable, forkJoin, mapTo, tap } from 'rxjs';

@Component({
  selector: 'app-report-generator',
  templateUrl: './report-generator.component.html',
  styleUrls: ['./report-generator.component.css']
})
export class ReportGeneratorComponent implements OnInit {

  public filterOptions = ["Линейная", "Гистограмма",
    "Круговая", "Диаграмма рассеяния"];
  public selectedReport: any = null;

  public schoolOptions: School[] = [];
  public selectedSchoolCodes: number[] = [];
  public classOptions = ["11A", "11B"];
  public typeOptions = ["ЕГЭ", "ОГЭ"];
  public subjectOptions = ["Математика", "Русский"];
  public schoolAverages: SchoolAverage[] = [];

  constructor(private reportService: ReportsService) { }

  ngOnInit(): void {
    this.getSchools();
  }

  getSchools(): void {
    this.reportService.getSchools()
      .subscribe(
        schools => {
          this.schoolOptions = schools;
        });
  }

  getSchoolsAverage(): Observable<void> {
    return this.reportService.getSchoolsAverage(this.selectedSchoolCodes).pipe(
      tap(schoolAverages => {
        this.schoolAverages = schoolAverages;
      }),
      mapTo(undefined)
    );
  }

  ngAfterViewInit(): void {
    //this.getDataAndCreateChart();
  }

  generateReport(): void {
    this.getSchoolsAverage().subscribe(() => {
      this.createChart();
    });
  }

  createChart(): void {
    const existingChart = Chart.getChart('my-chart');
    if (existingChart) {
      existingChart.destroy();
    }

    Chart.register(...registerables);
    const data = {
      labels: this.selectedSchoolCodes,
      datasets: [{
        label: 'Среднее по школам',
        
        data: this.schoolAverages.map(schoolAverage => schoolAverage.averageSecondaryPoints),
      }]
    };

    const options = {
      scales: {
        y: {
          beginAtZero: true,
          display: false
        }
      }
    }

    let type: keyof ChartTypeRegistry = 'bar'; 

    switch (this.selectedReport) {
      case 'Линейная': type = 'line'; break;
      case 'Гистограмма': type = 'bar'; break;
      case 'Круговая': type = 'pie'; break;
      case 'Диаграмма рассеяния': type = 'scatter'; break;
    }

    const config: ChartConfiguration = {
      type: type,
      data: data,
      options: options
    }

    const chartItem: ChartItem = document.getElementById('my-chart') as ChartItem
    new Chart(chartItem, config)
  }

}
