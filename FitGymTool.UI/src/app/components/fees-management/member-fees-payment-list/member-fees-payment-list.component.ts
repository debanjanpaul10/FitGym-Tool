import {
  Component,
  Input,
  OnChanges,
  signal,
  SimpleChanges,
  WritableSignal,
} from '@angular/core';
import { CurrentMembersFeesStatusDTO } from '@models/DTO/current-members-fees-status-dto.model';

@Component({
  selector: 'app-member-fees-payment-list',
  imports: [],
  templateUrl: './member-fees-payment-list.component.html',
  styleUrl: './member-fees-payment-list.component.scss',
})
export class MemberFeesPaymentListComponent implements OnChanges {
  @Input() feesStatusData: WritableSignal<CurrentMembersFeesStatusDTO[]> =
    signal([]);

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['feesStatusData']) {
      console.log(this.feesStatusData());
    }
  }
}
