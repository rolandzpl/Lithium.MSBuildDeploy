# Lithium.MSBuildDeploy

Contains MSBuild tasks that enableeasy deployment to linux environments.

## Install package

````
dotnet add package Lithium.MSBuildDeploy -v 0.0.0-*
````

Commands contained in package and available in MSBuild:
1. Sftp
1. SshCommand

After package gets installed, a target **Deploy**, contained in file Lithium.MSBuildDeploy.targets
, will automatically be available in your MSBuild file or project file.

## Sftp

_Sftp_ is a task that allows to coopy files from source to destination (usually remote).

Parameters:
DeploySource
:	Source folder from where deployed files are obtained

DeployTarget
:	Target folder (on remote machine usually)

DeployHost
:	Remote host of deployment

DeployPort
:	Remote port (usually it would be 22)

DeployUser
:	User on remote host

DeployKeyFile
:	Key file used toauthenticate on a remote host

Example:
````xml
<PropertyGroup>
    <DeploySource Condition="$('DeploySource') == ''">.\bin\$(Configuration)\$(TargetFramework)</DeploySource>
    <DeployTarget Condition="$('DeployTarget') == ''">/home/pi/test</DeployTarget>
    <DeployHost Condition="$('DeployHost') == ''">192.168.0.192</DeployHost>
    <DeployPort Condition="$('DeployPort') == ''">22</DeployPort>
    <DeployUser Condition="$('DeployUser') == ''">pi</DeployUser>
    <DeployKeyFile Condition="$('DeployKeyFile') == ''">lithium.key</DeployKeyFile>
</PropertyGroup>
````

It specifies deployment to remote host **192.168.0.192** using port **22** as user **pi**.
For authenticating user a key **lithium.key** will be used. Files are coppied from
**.\bin\$(Configuration)\$(TargetFramework)** local directory to destination **/home/pi/test**.

Note variables **$(Configuration)** and **$(TargetFramework)** in source path.
They allow to parameterize path depending on the build.

## SshCommand

_SshCommand_ ia a task that allows torun command over _SSH_.

Parameters:
DeployHost
:	Remote host of deployment

DeployPort
:	Remote port (usually it would be 22)

DeployUser
:	User on remote host

DeployKeyFile
:	Key file used toauthenticate on a remote host

Command
:   Command to execute, i.e.`ls -al`.