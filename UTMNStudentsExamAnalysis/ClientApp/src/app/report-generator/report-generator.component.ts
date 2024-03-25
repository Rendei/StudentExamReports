import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-report-generator',
  templateUrl: './report-generator.component.html',
  styleUrls: ['./report-generator.component.css']
})
export class ReportGeneratorComponent implements OnInit {
  public filterOptions = ["Таблица", "Гистограмма",
    "Круговая диаграмма", "Диаграмма рассеяния"];

  public schoolOptions = ["63", "83"];
  public classOptions = ["11A", "11B"];
  constructor() { }

  ngOnInit(): void {
  }

}
