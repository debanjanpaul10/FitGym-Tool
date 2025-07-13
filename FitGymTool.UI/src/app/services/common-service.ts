import { Injectable, signal, WritableSignal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  private mappingMasterData: WritableSignal<any> = signal(null);

  public get MappingMasterData() {
    return this.mappingMasterData();
  }

  public set MappingMasterData(data: any) {
    this.mappingMasterData.set(data);
  }
}
