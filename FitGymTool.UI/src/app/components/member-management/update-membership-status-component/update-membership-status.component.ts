import {
  Component,
  EventEmitter,
  inject,
  Input,
  OnDestroy,
  OnInit,
  Output,
  signal,
  WritableSignal,
} from '@angular/core';
import { DialogPopupService } from '@core/services/dialog-popup.service';
import { MembershipStatusMappingDto } from '@models/DTO/Mapping/membership-status-mapping-dto.model';
import { MemberDetailsDto } from '@models/DTO/memberdetails-dto.model';

@Component({
  selector: 'app-update-membership-status-component',
  imports: [],
  templateUrl: './update-membership-status.component.html',
  styleUrl: './update-membership-status.component.scss',
})
export class UpdateMembershipStatusComponent {
  @Input() public memberDetails: MemberDetailsDto[] | null = null;
  @Output() public membershipStatusUpdate: EventEmitter<void> =
    new EventEmitter<void>();

  protected visible: WritableSignal<boolean> = signal(false);
  protected membershipStatusOptions: MembershipStatusMappingDto[] = [];

  private readonly dialogPopupService: DialogPopupService =
    inject(DialogPopupService);

  constructor() {
    this.visible = this.dialogPopupService.isUpdateMembershipDialogOpen;
  }
}
