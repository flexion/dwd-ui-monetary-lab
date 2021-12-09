# Development Guidelines

Table of contents:
- [Introduction and Goals](#introduction-and-goals-)
- [Git](#git-)
  - [Cloning a DWD repository](#cloning-a-dwd-repository-)
  - [Healthy git practices](#healthy-git-practices-)
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

## Healthy git practices [^](#)

- Create useful commit messages:
  ```
  1st line is a short description of the changeset

  3rd+ lines are the explanation of the change. Diffs can show us "what" changed,
    but comments can tell us "why" it changed. Here is a good place to include:
    - issue #'s,
    - decision points,
    - failed experiments,
    - references
    - etc.

    A good starting point is to put what the state/behavior was before, what it
    is now, and why that solution was chosen.
  ```
- Periodically rebase feature branches
  - Rather than merging `main` into your feature branch (which creates merge commits on your branch),
    [rebase](https://git-scm.com/book/en/v2/Git-Branching-Rebasing) your changes on top of `main`. This can
    be done with a configuration:
    ```bash
    git config --global pull.rebase true
    #git pull origin main
    ```
- Refactor commits before Pull Request
  - any branch you create is your history. Rewriting history is not a problem on these branches because
    you own them and they are temporary anyway. Make as many work in progress commits as you like. When you
    have the code ready to be integrated, use
    [git rebase](https://git-scm.com/book/en/v2/Git-Tools-Rewriting-History#_squashing) to chunk the commits
    into logical change sets (*a formally collected set of changes that should be treated as a group*) and
    push (may require `--force`). This can greatly help readability of the git history and ease in reverting
    changes.

    ---
    ***⚠ Note:** rebasing may not go smoothly sometimes, but a failed attempt can be undone. It is useful to
    create a branch or understanding the [git reflog](https://git-scm.com/docs/git-reflog) command before
    starting so that you can always return to the pre-rebased changes.*

    ---
  - Integration branches such as main are immutable. Once a pull request has been approved, a new change has to
    then be approved to modify it. The history is now a permanent record.
- Avoid adding generated files
  - Generated files, such as reports (ie coverage reports), debugging files (ie .pdbs), etc should not be put
    into git. Be sure to add them to the gitignore when applicable or accidental/unnecessary changes can be
    introduced.
- Avoid adding binary files
  - Binary files, such as zip files, libraries, etc should not be in git. Modern languages manage dependencies
    through dependency or tool managers such as [Nuget](https://www.nuget.org/).



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