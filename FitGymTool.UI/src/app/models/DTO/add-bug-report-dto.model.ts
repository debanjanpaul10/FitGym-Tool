/**
 * Data transfer object for submitting bug reports.
 * Contains all necessary information required to create a new bug report
 * including title, description, severity level, and context information.
 */
export class AddBugReportDTO {
  public bugTitle: string = '';
  public bugDescription: string = '';
  public bugSeverity: number = 0;
  public createdBy: string = '';
  public pageUrl: string = '';
}
