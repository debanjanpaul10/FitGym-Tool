import { Component, inject, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

import { ActiveMembersComponent } from '@components/dashboard/active-members-component/active-members.component';
import { CurrentRevenueComponent } from '@components/dashboard/current-revenue-component/current-revenue.component';
import { CommonApiService } from '@services/common-api.service';
import { LoaderService } from '@core/services/loader.service';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ToasterService } from '@core/services/toaster.service';
import { CommonService } from '@core/services/common.service';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { CurrentUserComponent } from '@components/dashboard/current-user-component/current-user.component';
import { AiStatusComponent } from '@components/dashboard/ai-status-component/ai-status.component';
import { AiFeaturesComponent } from '@components/dashboard/ai-features-component/ai-features.component';

@Component({
  selector: 'app-dashboard',
  imports: [
    CardModule,
    ButtonModule,
    CurrentRevenueComponent,
    ActiveMembersComponent,
    CurrentUserComponent,
    AiStatusComponent,
    AiFeaturesComponent,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
  protected mappingsMasterData: MasterMappingDataDto =
    new MasterMappingDataDto();

  private readonly commonApiService: CommonApiService =
    inject(CommonApiService);
  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly toasterService: ToasterService = inject(ToasterService);
  private readonly commonService: CommonService = inject(CommonService);

  ngOnInit(): void {
    this.getMappingsMasterData();
  }

  private getMappingsMasterData(): void {
    this.loaderService.loadingOn();

    this.commonApiService.GetMappingsMasterDataAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response && response?.isSuccess) {
          this.mappingsMasterData = response.responseData;
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
