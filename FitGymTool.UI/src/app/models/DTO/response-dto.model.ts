export class ResponseDto {
  public statusCode: number;

  public isSuccess: boolean;

  public responseData: any;

  constructor(StatusCode: number, IsSuccess: boolean, ResponseData: any) {
    this.statusCode = StatusCode;
    this.isSuccess = IsSuccess;
    this.responseData = ResponseData;
  }
}
