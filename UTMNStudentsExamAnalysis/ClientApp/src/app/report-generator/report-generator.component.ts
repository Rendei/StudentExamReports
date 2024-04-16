import { Component, OnInit, Renderer2 } from '@angular/core';
import { Chart, ChartConfiguration, ChartItem, ChartTypeRegistry, registerables } from 'chart.js'
import { School } from './models/school';

import { ReportsService } from '../reports.service'
import { ClassAverage, SchoolAverage } from './models/averages';
import { Class } from './models/class';
import { Subject } from './models/subject';

@Component({
  selector: 'app-report-generator',
  templateUrl: './report-generator.component.html',
  styleUrls: ['./report-generator.component.css']
})
export class ReportGeneratorComponent implements OnInit {

  public charts: ChartWithCanvas[] = [];
  public filterOptions = ["Линейная", "Гистограмма",
    "Круговая", "Диаграмма рассеяния"];
  public selectedReport: any = null;

  public schoolOptions: School[] = [];
  public selectedSchoolCodes: number[] = [];
  public schoolAverages: SchoolAverage[] = [];

  public classOptions: Class[] = [];
  public selectedSchoolClasses: string[] = [];
  public classAverages: ClassAverage[] = [];

  public typeOptions = ["ЕГЭ", "ОГЭ"];

  public subjectOptions: Subject[] = [];
  public selectedSubjects: number[] = [];

  public yearOptions: number[] = [];
  public selectedYears: number[] = [];

  constructor(private reportService: ReportsService, private renderer: Renderer2) { }

  ngOnInit(): void {
    this.reportService.getSchools().subscribe(
        schools => {
          this.schoolOptions = schools;
        });

    this.reportService.getSubjects().subscribe(
      subjects => {
        this.subjectOptions = subjects;
      });

    this.reportService.getYears().subscribe(
      years => {
        this.yearOptions = years;
      });
    
  }


  ngAfterViewInit(): void {
    //this.getDataAndCreateChart();
  }

  generateReport(): void {
    if (this.selectedSchoolClasses.length > 0) {
      this.reportService.getSchoolClassesAverage(this.selectedSchoolCodes[0], this.selectedSchoolClasses).subscribe(classAverages => {
        this.classAverages = classAverages;
        let xData = this.classAverages.map(classAverage => classAverage.averageSecondaryPoints);
        let yData = this.classAverages.map(classAverage => classAverage.className);
        this.createChart(xData, yData);
      })
    }
    else {
      this.reportService.getSchoolsAverage(this.selectedSchoolCodes, this.selectedSubjects, this.selectedYears  ).subscribe(schoolAverages => {
        this.schoolAverages = schoolAverages;
        let xData = this.schoolAverages.map(schoolAverage => schoolAverage.averageSecondaryPoints);
        let yData = this.schoolAverages.map(schoolAverage => schoolAverage.shortName);
        this.createChart(xData, yData);
      });
    }
    
  }

  createChart(xData: Array<any>, yData: Array<any>): void {
    const button: HTMLButtonElement = this.renderer.createElement('button');
    button.textContent = "X";
    this.renderer.addClass(button, "close-button");
    const parent = document.getElementById('chartContainer');
    this.renderer.appendChild(parent, button);

    const canvas = this.renderer.createElement('canvas');
    this.renderer.addClass(canvas, 'chart');   
    this.renderer.appendChild(parent, canvas);

    

    Chart.register(...registerables);
    const data = {
      labels: yData,
      datasets: [{
        label: 'Среднее по школам',
        
        data: xData,
      }]
    };

    const options = {
      scales: {
        y: {
          beginAtZero: true,
          display: true
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

    const chart = new Chart(canvas, config);
    this.charts.push({ chart, canvas });
    // TODO add onclick deleteChart
    //button.addEventListener('click', this.deleteChart.bind(this.charts.length - 1), false); 
    //button.
  }

  deleteChart(index: number): void {
    if (index >= 0 && index < this.charts.length) {
      const { chart, canvas } = this.charts[index];
      canvas.remove();
      this.charts.splice(index, 1);
      chart.destroy();
    } else {
      console.log('Invalid chart index.');
    }
  }

  onSelectionChange(): void {
    this.selectedSchoolClasses = [];
    if (this.selectedSchoolCodes.length === 1) {
      this.reportService.getSchoolClasses(this.selectedSchoolCodes[0]).subscribe(classes => this.classOptions = classes);
    }
    else {
      this.classOptions = [];      
    }
  }

}


type ChartWithCanvas = {
  chart: Chart,
  canvas: HTMLCanvasElement
};
