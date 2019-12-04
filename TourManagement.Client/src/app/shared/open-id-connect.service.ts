import {Injectable} from '@angular/core';
import {UserManager, User} from 'oidc-client';
import {environment} from "../../environments/environment";
import {ReplaySubject} from 'rxjs/ReplaySubject';

@Injectable()
export class OpenIdConnectService {

  private userManager: UserManager = new UserManager(environment.openIdConnectService);
  private currentUser: User;

  $userLoaded = new ReplaySubject<boolean>(1);

  get userAvailable(): boolean {
    return this.currentUser != null;
  }

  get user(): User {
    return this.currentUser;
  }

  constructor() {
    this.userManager.clearStaleState();

    this.userManager.events.addUserLoaded(user => {
      if (!environment.production) {
        console.log('User loaded', user);
      }
      this.currentUser = user;
      this.$userLoaded.next(true);
    });

    this.userManager.events.addUserUnloaded(() => {
      if (!environment.production) {
        console.log('User unloaded');
      }
      this.currentUser = null;
      this.$userLoaded.next(false);
    });
  }

  triggerSignIn() {
    this.userManager.signinRedirect().then(() => {
      if (!environment.production) {
        console.log('Rediretion to signin triggered');
      }
    })
  }

  triggerSignOut() {
    this.userManager.signoutRedirect().then((resp) => {
      if (!environment.production) {
        console.log('Rediretion to sign out triggered', resp);
      }
    })
  }

  handleCallBack() {
    this.userManager.signinRedirectCallback().then((user) => {
      if (!environment.production) {
        console.log('Callback after signing handled. ', user);
      }
    })
  }

  handleSilentCallback() {
    this.userManager.signinSilentCallback().then((user) => {
      this.currentUser = user;
      if (!environment.production) {
        console.log('Callback after silent signin handled. ', user);
      }
    })
  }

}
