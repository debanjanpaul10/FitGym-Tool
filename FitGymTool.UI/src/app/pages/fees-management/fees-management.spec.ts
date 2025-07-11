import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeesManagement } from './fees-management';

describe('FeesManagement', () => {
  let component: FeesManagement;
  let fixture: ComponentFixture<FeesManagement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FeesManagement]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FeesManagement);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
