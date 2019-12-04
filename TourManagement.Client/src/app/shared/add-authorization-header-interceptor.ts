import {Injectable} from "@angular/core";
import {OpenIdConnectService} from "./open-id-connect.service";
import {HttpInterceptor, HttpRequest, HttpHandler, HttpEvent} from "@angular/common/http";
import {Observable} from "rxjs/Observable";

@Injectable()
export class AddAuthorizationHeaderInterceptor implements HttpInterceptor {
  constructor(private openIdConnectService: OpenIdConnectService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add access token as bearer token
    request = request.clone(
      {
        setHeaders: {
          Authorization: this.openIdConnectService.user.token_type + ' '
            + this.openIdConnectService.user.access_token
        }
      });
    return next.handle(request);
  }
}
