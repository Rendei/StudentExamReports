import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TownTypeListComponent } from './town-type-list.component';

describe('TownTypeListComponent', () => {
  let component: TownTypeListComponent;
  let fixture: ComponentFixture<TownTypeListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TownTypeListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TownTypeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
