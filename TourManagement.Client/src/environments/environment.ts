// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  apiUrl: 'http://localhost:51114/api',
  // apiUrl: 'https://localhost:44353/api'
  openIdConnectService: {
    authority: 'http://localhost:60940/',
    // authority: 'https://localhost:44398/',
    client_id: 'tourmanagementclient',
    redirect_uri: 'http://localhost:4200/signin-oidc',
    // redirect_uri: 'https://localhost:4200/signin-oidc',
    scope: 'openid profile roles tourmanagementapi',
    response_type: 'id_token token',
    post_logout_redirect_uri: 'http://localhost:4200/'
    // post_logout_redirect_uri: 'https://localhost:4200/'
  }
};
