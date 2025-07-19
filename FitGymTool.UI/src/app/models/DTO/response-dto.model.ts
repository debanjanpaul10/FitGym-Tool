/**
 * Generic data transfer object for API responses.
 * Standardizes the structure of all API responses with status information,
 * success indicators, and response data payload.
 */
export class ResponseDto {
  public statusCode: number = 0;
  public isSuccess: boolean = false;
  public responseData: any = [];
}
