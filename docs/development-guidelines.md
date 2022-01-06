# Development Guidelines

Table of contents:
- [Development Guidelines](#development-guidelines)
  - [Introduction and Goals](#introduction-and-goals-)
  - [Git](#git-)
    - [Cloning a DWD repository](#cloning-a-dwd-repository-)
    - [Setting up GPG](#setting-up-gpg-)
    - [LFS](#lfs-)
  - [Topic X](#topic-x-)
  - [Topic Y](#topic-y-)
  - [Topic Z](#topic-z-)

## Introduction and Goals [^](#development-guidelines)
This document consists of a collection of general development guidelines to help ease maintenance and development. These suggestions, as most guidelines, are subject to change as we learn more.

The goals of this document are as follows:
- Explain code structure
- Assist new engineers as they are introduced to the work / the team
- Explain team decisions and approach
- Placeholder
- Placeholder
- Placeholder

## Git [^](#development-guidelines)

Git is the Version Control System (VCS) used for the DWD UI project. The Version Control System is **mainly** responsible for:

- Recording changes over time
- Allowing for concurrent development (using branching and merging)

By following a few healthy practices, the VCS can also provide:

- A readable story around how the code has changed over time
- A mitigation against merge conflicts and rework
- An easy process for troubleshooting and repairing ([bisect](https://git-scm.com/docs/git-bisect),
  [revert](https://git-scm.com/docs/git-revert))

### Cloning a DWD repository [^](#development-guidelines)

Since DWD uses a Single Sign On, some extra configuration is necessary for cloning the repository.

In GitHub, login as your WI-DWD account (first-last_widwd).

Once logged in, create a *`Personal Access Token`* (ie PAT) with the full `repo` scope by following
[these instructions](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token).

Add SSO to that `PAT` with [these instructions](https://docs.github.com/en/enterprise-cloud@latest/authentication/authenticating-with-saml-single-sign-on/authorizing-a-personal-access-token-for-use-with-saml-single-sign-on).

> ---
> ⚠ Warning
>
> ---
> Treat this token as a password and keep it safe (preferably in a password storage program). If the password is
> compromised, you can easily revoke it and generate a new one.
>
> ---

Once you have a token with SSO enabled, you can clone by inserting your GitHub user id before the clone URL.

For example:
- the repo URL: `https://github.com/WI-DWD/UI-Modernization.git`
- the clone URL: `https://`**firstname-lastname_widwd@**`github.com/WI-DWD/UI-Modernization.git`

```bash
# Running this should prompt you for a password.
# When prompted, enter the PAT that we created above
git clone https://firstname-lastname_widwd@github.com/WI-DWD/UI-Modernization.git`
```

### Setting up GPG [^](#development-guidelines)
Need:
- Installed version of the [gpg](https://www.gnupg.org/download/#sec-1-2) command line
- Have your wisconsin.gov email ready(firstname.lastname@wisconsin.gov)

To create your new key:
- [Create a new GPG key](https://docs.github.com/en/authentication/managing-commit-signature-verification/generating-a-new-gpg-key)
- [Add your new key to your GH account](https://docs.github.com/en/authentication/managing-commit-signature-verification/adding-a-new-gpg-key-to-your-github-account)

Next, you can set git to use that key for all your repos or for specific repos.
- All Repos:
  ```bash
  git config --global commit.gpgsign true
  KEY_ID=$(gpg --list-secret-keys --keyid-format=long wisconsin.gov | grep ^sec | sed -e 's|.*/\([A-Z0-9]*\) .*|\1|')
  git config --global user.signingkey $KEY_ID
  ```

- Specific Repos:

  ```bash
  # First, clone the repository that you want to use this key for.
  # Change to that cloned directory and run:
  git config --local commit.gpgsign true
  KEY_ID=$(gpg --list-secret-keys --keyid-format=long wisconsin.gov | grep ^sec | sed -e 's|.*/\([A-Z0-9]*\) .*|\1|')
  git config --local user.signingkey $KEY_ID
  git config --local user.email firstname.lastname@wisconsin.gov
  git config --local user.name "Firstname Lastname"
  ```

### LFS [^](#development-guidelines)

Git LFS is an extra system used to store large, typically non-text files in a source repository. Because of the way that git checks for changes, these files can cause issues in the size and performance of git.

Good candidates for git LFS are:
- Audio/Video Files
- CAD/3D data files
- Large image files

For the DWD UI, we don't currently see a need for the extra configuration and management required for git LFS. Any icon files, typically sized image files, etc, can be added to the repo directly without significant overhead.

---
***⚠ Note:** If larger files are required, this may change.*

---

## Topic X [^](#development-guidelines)
Placeholder

## Topic Y [^](#development-guidelines)
Placeholder

## Topic Z [^](#development-guidelines)
Placeholder
