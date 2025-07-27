import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Button } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { InputText } from 'primeng/inputtext';
import { IftaLabel } from 'primeng/iftalabel';
import { Textarea } from 'primeng/textarea';
import { MessageModule } from 'primeng/message';

import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { AddMemberDto } from '@models/DTO/members/add-member-dto.model';

/**
 * Component responsible for the final validation and review step of the member registration process.
 * Displays a comprehensive, read-only summary of all member data collected in previous steps.
 * All form fields are disabled to prevent direct editing - users must navigate back to previous
 * steps to make changes. Provides clear navigation controls to edit personal details (step 1)
 * or subscription information (step 2). Changes made in previous steps are automatically
 * reflected when returning to this validation view, ensuring data consistency throughout
 * the multi-step registration workflow.
 */
@Component({
  selector: 'app-final-validation-form',
  standalone: true,
  imports: [
    Button,
    CommonModule,
    IftaLabel,
    InputText,
    Textarea,
    MessageModule,
  ],
  templateUrl: './final-validation-form.component.html',
  styleUrl: './final-validation-form.component.scss',
})
export class FinalValidationFormComponent {
  @Input() masterMappingData: MasterMappingDataDto = new MasterMappingDataDto();
  @Input() set currentMemberData(value: AddMemberDto) {
    this._currentMemberData = value;
  }
  get currentMemberData(): AddMemberDto {
    return this._currentMemberData;
  }
  @Input() currentStep: number = 3;
  @Input() visible: boolean = false;

  private _currentMemberData: AddMemberDto = new AddMemberDto();
  @Output() currentStepChange = new EventEmitter<number>();
  @Output() newMemberData: EventEmitter<AddMemberDto> =
    new EventEmitter<AddMemberDto>();
  @Output() visibleChange: EventEmitter<boolean> = new EventEmitter<boolean>();

  /**
   * Handles the submission of the final member data after validation.
   * Emits the complete member data to the parent component for processing and database storage.
   * This represents the final step in the member registration workflow.
   */
  protected submitFinalMemberData(): void {
    this.newMemberData.emit(this.currentMemberData);
    this.visible = false;
    this.visibleChange.emit(this.visible);
  }

  /**
   * Handles the cancellation of the entire member registration process.
   * Closes the dialog by setting visibility to false and notifies parent components of the change.
   * No data is saved when this action is performed.
   */
  protected onCancel(): void {
    this.visible = false;
    this.visibleChange.emit(this.visible);
  }

  /**
   * Navigates back to a specific step in the member registration process.
   * Allows users to edit information from previous steps and return to validation.
   * @param step The step number to navigate to (1 for personal details, 2 for subscription)
   */
  protected goToStep(step: number): void {
    this.currentStepChange.emit(step);
  }

  /**
   * Formats a date object to a readable string format (dd/mm/yyyy).
   * Returns 'N/A' if the date is null or undefined.
   */
  protected formatDate(date: Date | null | undefined): string {
    if (!date) return 'N/A';

    try {
      const dateObj = new Date(date);
      if (isNaN(dateObj.getTime())) return 'N/A';

      const day = dateObj.getDate().toString().padStart(2, '0');
      const month = (dateObj.getMonth() + 1).toString().padStart(2, '0');
      const year = dateObj.getFullYear();

      return `${day}/${month}/${year}`;
    } catch (error) {
      console.error('Error formatting date:', error);
      return 'N/A';
    }
  }
}
