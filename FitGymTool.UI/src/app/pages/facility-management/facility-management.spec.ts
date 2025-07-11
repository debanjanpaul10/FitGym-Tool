import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FacilityManagement } from './facility-management';

describe('FacilityManagement', () => {
  let component: FacilityManagement;
  let fixture: ComponentFixture<FacilityManagement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FacilityManagement]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FacilityManagement);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
