import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

import { ActiveMembersComponent } from '@components/dashboard/active-members-component/active-members.component';
import { CurrentRevenueComponent } from '@components/dashboard/current-revenue-component/current-revenue.component';
import { CommonApiService } from '@core/services/common-api.service';
import { LoaderService } from '@services/loader.service';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ToasterService } from '@services/toaster.service';
import { CommonService } from '@services/common.service';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';

@Component({
  selector: 'app-dashboard',
  imports: [
    CardModule,
    ButtonModule,
    CurrentRevenueComponent,
    ActiveMembersComponent,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
  public mappingsMasterData: WritableSignal<MasterMappingDataDto> = signal(
    new MasterMappingDataDto()
  );

  private readonly commonApiService: CommonApiService =
    inject(CommonApiService);
  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly toasterService: ToasterService = inject(ToasterService);
  private readonly commonService: CommonService = inject(CommonService);

  ngOnInit(): void {
    this.loaderService.loadingOn();

    this.commonApiService.GetMappingsMasterDataAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response && response?.isSuccess) {
          this.mappingsMasterData.set(response.responseData);
          this.commonService.MappingMasterData = response.responseData;
        } else {
          this.toasterService.showError(response?.responseData);
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
