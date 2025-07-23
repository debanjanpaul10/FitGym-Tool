import {
  Component,
  inject,
  OnInit,
  signal,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import { Table, TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';

import { ToasterService } from '@core/services/toaster.service';
import { FeesStructureDTO } from '@models/DTO/fees-structure-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { MemberFeesApiService } from '@services/member-fees-api.service';
import { SkeletonModule } from 'primeng/skeleton';
import { Column } from '@models/interfaces/column.interface';
import { FeesManagementConstants } from '@shared/application.constants';

@Component({
  selector: 'app-current-fees-structure',
  imports: [TableModule, CommonModule, SkeletonModule],
  templateUrl: './current-fees-structure.component.html',
  styleUrl: './current-fees-structure.component.scss',
})
export class CurrentFeesStructureComponent implements OnInit {
  @ViewChild('fstable') fstable!: Table;

  protected feesStructureData: WritableSignal<FeesStructureDTO[]> = signal([]);
  protected isFeesStructureLoading: WritableSignal<boolean> = signal(false);
  protected columnHeaders: Column[] = [];
  protected currentFeesStructureConstants =
    FeesManagementConstants.CurrentFeesStructureConstants;

  private readonly _memberFeesApiService: MemberFeesApiService =
    inject(MemberFeesApiService);
  private readonly _toasterService: ToasterService = inject(ToasterService);

  constructor() {
    this.columnHeaders = [
      { field: 'feesDuration', header: 'Duration' },
      { field: 'feesAmount', header: 'Amount (₹)' },
    ];
  }

  ngOnInit(): void {
    this.loadCurrentFeesStructure();
  }

  private loadCurrentFeesStructure(): void {
    this.isFeesStructureLoading.set(true);

    this._memberFeesApiService.GetCurrentFeesStructureAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response?.isSuccess && response?.responseData) {
          this.feesStructureData.set(response.responseData);
        } else {
          this._toasterService.showError(response?.responseData);
        }
      },
      error: (error: Error) => {
        this.isFeesStructureLoading.set(false);
        console.error(error);
        this._toasterService.showError(error.message);
      },
      complete: () => {
        this.isFeesStructureLoading.set(false);
      },
    });
  }
}
