import { Component, OnInit } from '@angular/core';
import { TownType } from '../town-type';
import { TownTypeService } from '../town-type.service';

@Component({
  selector: 'app-town-type-list',
  templateUrl: './town-type-list.component.html',
  styleUrls: ['./town-type-list.component.css']
})
export class TownTypeListComponent implements OnInit {
  townTypes: TownType[] = [];

  constructor(private townTypeService: TownTypeService) { }

  ngOnInit(): void {
    this.getTownTypes();
  }

  getTownTypes(): void {
    this.townTypeService.getTownTypes()
      .subscribe(townTypes => this.townTypes = townTypes);
  }
}
