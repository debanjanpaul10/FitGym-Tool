import {
  Component,
  inject,
  OnDestroy,
  OnInit,
  signal,
  WritableSignal,
  Output,
  EventEmitter,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { SelectModule } from 'primeng/select';
import { TextareaModule } from 'primeng/textarea';

import { AddMemberDto } from '@models/DTO/add-member-dto.model';
import { DialogPopupService } from '@core/services/dialog-popup.service';
import {
  MemberManagementConstants,
  ToasterSuccessMessages,
} from '@shared/application.constants';
import { CommonService } from '@core/services/common.service';
import { MembershipStatusMappingDto } from '@models/DTO/Mapping/membership-status-mapping-dto.model';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { CommonApiService } from '@services/common-api.service';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { LoaderService } from '@core/services/loader.service';
import { ToasterService } from '@services/toaster.service';
import { MembersApiService } from '@services/members-api.service';

@Component({
  selector: 'app-add-user-component',
  imports: [
    DialogModule,
    ButtonModule,
    ReactiveFormsModule,
    InputTextModule,
    TextareaModule,
    DatePickerModule,
    SelectModule,
  ],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.scss',
})
export class AddUserComponent implements OnInit, OnDestroy {
  @Output() memberAdded: EventEmitter<void> = new EventEmitter<void>();

  protected visible: WritableSignal<boolean> = signal(false);
  protected memberForm: FormGroup;
  protected addMemberConstants =
    MemberManagementConstants.AddNewMemberConstants;
  protected genderOptions = this.addMemberConstants.genderOptions;
  protected membershipStatusOptions: MembershipStatusMappingDto[] = [];

  private mappingMasterDataSubscription: any;

  private readonly dialogPopupService: DialogPopupService =
    inject(DialogPopupService);
  private readonly formBuilder: FormBuilder = inject(FormBuilder);
  private readonly commonService: CommonService = inject(CommonService);
  private readonly commonApiService: CommonApiService =
    inject(CommonApiService);
  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly toasterService: ToasterService = inject(ToasterService);
  private readonly membersApiService: MembersApiService =
    inject(MembersApiService);

  constructor() {
    this.visible = this.dialogPopupService.isAddMemberDialogOpen;
    this.memberForm = this.createForm();
  }

  ngOnInit(): void {
    this.mappingMasterDataSubscription =
      this.commonService.MappingMasterData.subscribe(
        (data: MasterMappingDataDto | null) => {
          if (
            data &&
            data?.membershipStatusMapping &&
            Object.values(data?.membershipStatusMapping).length > 0
          ) {
            this.membershipStatusOptions = data?.membershipStatusMapping;
          } else {
            this.getMasterMappingsData();
          }
        }
      );
  }

  ngOnDestroy(): void {
    if (this.mappingMasterDataSubscription) {
      this.mappingMasterDataSubscription.unsubscribe();
    }
  }

  protected submitNewUserForm(): void {
    this.loaderService.loadingOn();
    if (this.memberForm.valid) {
      const memberData: AddMemberDto = {
        memberName: this.memberForm.value.memberName,
        memberEmail: this.memberForm.value.memberEmail || null,
        memberPhoneNumber: this.memberForm.value.memberPhoneNumber,
        memberAddress: this.memberForm.value.memberAddress,
        memberDateOfBirth: this.memberForm.value.memberDateOfBirth,
        memberGender: this.memberForm.value.memberGender,
        memberJoinDate: this.memberForm.value.memberJoinDate,
        membershipStatus: this.memberForm.value.membershipStatus,
      };

      this.membersApiService.AddNewMemberAsync_FromAdmin(memberData).subscribe({
        next: (response: ResponseDto) => {
          if (response.isSuccess && response.responseData) {
            this.toasterService.showSuccess(
              ToasterSuccessMessages.MemberManagement.AddMemberSuccess
            );
            this.memberAdded.emit();
            this.onCancel();
          } else {
            this.toasterService.showError(response.responseData);
          }
        },
        error: (err: Error) => {
          this.loaderService.loadingOff();
          console.error(err.message);
          this.toasterService.showError(err.message);
        },
        complete: () => {
          this.loaderService.loadingOff();
        },
      });
    }
  }

  protected onCancel(): void {
    this.memberForm.reset();
    this.memberForm.patchValue({
      memberJoinDate: new Date(),
    });
    this.visible.set(false);
  }

  private createForm(): FormGroup {
    return this.formBuilder.group({
      memberName: ['', [Validators.required, Validators.minLength(2)]],
      memberEmail: ['', [Validators.email]],
      memberPhoneNumber: [
        '',
        [Validators.required, Validators.pattern(/^\d{10,15}$/)],
      ],
      memberAddress: ['', [Validators.required, Validators.minLength(5)]],
      memberDateOfBirth: [null, [Validators.required]],
      memberGender: ['', [Validators.required]],
      memberJoinDate: [new Date(), [Validators.required]],
      membershipStatus: ['', [Validators.required]],
    });
  }

  private getMasterMappingsData(): void {
    this.loaderService.loadingOn();
    this.commonApiService.GetMappingsMasterDataAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response && response.isSuccess) {
          this.membershipStatusOptions =
            response.responseData?.membershipStatusMapping;
        }
      },
      error: (err: Error) => {
        this.loaderService.loadingOff();
        console.error(err);
        this.toasterService.showError(err.message);
      },
      complete: () => {
        this.loaderService.loadingOff();
      },
    });
  }
}
