## Note on _TestEnvironment_

Class _TestEnvironment_ containe specific values revelant to my environment.
This means that if you want to run these tests you should change the values there.

## Connecting without password
To connect via _ssh_ without password you should create and copy ssh
public key to destination home directory:

To generate key:
````
> ssh-keygen -t rsa
Generating public/private rsa key pair.
Enter file in which to save the key (.../user/.ssh/id_rsa): 
Created directory '.../user/.ssh'.
Enter passphrase (empty for no passphrase): 
Enter same passphrase again: 
Your identification has been saved in .../user/.ssh/id_rsa.
Your public key has been saved in .../user/.ssh/id_rsa.pub.
The key fingerprint is:
SHA256:a3VDCUMp8jMC368OqwLlGyNIv3wDhbCrgENV2sjZZrA user@local-machine
````

Create _.ssh_ directory:
````
user@machine:~ $ ssh user@machine mkdir -p .ssh
user@machine's password: 
````
If the directory already exists, it will take no effect. It is safe to execute it.

Finally, copy public key to a target machine\w _authorized_keys_.
````
user@machine:~ $ cat .ssh/id_rsa.pub | ssh user@machine 'cat >> .ssh/authorized_keys'
user@machine's password: 
````

Since now you can login to remote machine without password:
````
> ssh user@machine
````

The private key, which was just created, must be used in the _TestEnvironment_ class then.
Tests should run.