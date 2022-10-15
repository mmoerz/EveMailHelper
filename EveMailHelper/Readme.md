# DAE Meldestelle webfrontend

## Package Management Console Packages

```
Install-Package NLog.Extensions.Logging
Install-Package NLog.Web.AspNetCore
Install-Package FluentValidation.AspNetCore
Install-Package Microsoft.AspNetCore.Authentication
Install-Package Microsoft.AspNetCore.Authentication.Core
Install-Package Microsoft.AspNetCore.Authentication.Negotiate
```

## Overview over Nupkg used
- Microsoft Authentication .net6 libs
- NLog (logging obviously - nice intro:
  [NLog Essentials](https://blog.elmah.io/nlog-tutorial-the-essential-guide-for-logging-from-csharp/)
  )
- [Mudblazor](https://mudblazor.com/) (UI Compontents library for blazor)

- FluentValidation (Ui input verification)

# authentication
- uses Windows Authentication
- adds database information to the user claims principal

for details see subdirectory readme.md

don't forget to enable windows authentication in the project settings (see below).

additionally a properly configured web.config is necessary
to force passing the windows authentication from the IIS to
the webapplication.
This mechanism should work properly regardless of out or in process configuration.

# packaging
simply deploy to a zip file.

# installation

## prerequisites
- IIS
- an installed .net 6.0 webhosting
- a preconfigured website
- an application pool that has been configured for exclusivly running this app
- webhosting deployment helpers for the iis manager

## steps
- select website
- deploy
- pick .zip
- after successful install:
    - set production variables in web.config:
      ```
      test
      ```


    - start/restart webapplication pool

# knowledge base
(MudBlazor with Fluent Validation)[https://mudblazor.com/components/form#using-fluent-validation]

# development notes
- set in the project's properties Debug>Enable Windows Auhtentication 
  and disable the Debug>Enable anonymous Authentication

## deployment notes
- needs web.config
- needs the custom lines in the .csproj

## connection string
{
  "ConnectionStrings": {
    "Default": "Persist Security Info=False;User ID=some;Password=Some;Initial Catalog=DAEMeldestelle;Server=localhost"
  }
}