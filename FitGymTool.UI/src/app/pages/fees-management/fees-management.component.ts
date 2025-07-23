import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';

import { CurrentFeesStructureComponent } from '@components/fees-management/current-fees-structure/current-fees-structure.component';
import { CurrentMemberFeesStatusComponent } from '@components/fees-management/current-member-fees-status/current-member-fees-status.component';
import { CommonService } from '@core/services/common.service';
import { LoaderService } from '@core/services/loader.service';
import { ToasterService } from '@core/services/toaster.service';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { CommonApiService } from '@services/common-api.service';

@Component({
  selector: 'app-fees-management',
  imports: [CurrentFeesStructureComponent, CurrentMemberFeesStatusComponent],
  templateUrl: './fees-management.component.html',
  styleUrl: './fees-management.component.scss',
})
export class FeesManagementComponent implements OnInit {
  protected mappingMasterData: WritableSignal<MasterMappingDataDto> = signal(
    new MasterMappingDataDto()
  );

  private readonly _commonApiService: CommonApiService =
    inject(CommonApiService);
  private readonly _loaderService: LoaderService = inject(LoaderService);
  private readonly _toasterService: ToasterService = inject(ToasterService);
  private readonly _commonService: CommonService = inject(CommonService);

  ngOnInit(): void {
    this._loaderService.loadingOn();

    this._commonApiService.GetMappingsMasterDataAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response?.isSuccess && response?.responseData) {
          this.mappingMasterData.set(response.responseData);
          this._commonService.MappingMasterData = response.responseData;
        } else {
          this._toasterService.showError(response?.responseData);
        }
      },
      error: (err: Error) => {
        this._loaderService.loadingOff();
        console.error(err);
        this._toasterService.showError(err.message);
      },
      complete: () => {
        this._loaderService.loadingOff();
      },
    });
  }
}
