Table of contents:
- [Overview](#overview-)
- [Git](#git-)
  - [LFS](#lfs-)

---

# Overview [^](#)

Here are a collection of general development guidelines to help ease maintenance and development. These
suggestions, as most guidelines, are subject to change as we learn more.

# Git [^](#)

Git is the Version Control System (VCS) used for the DWD UI project. The Version Control System is **mainly** 
responsible for:

- Recording changes over time
- Allowing for concurrent development (using branching and merging)

By following a few healthy practices, the VCS can also provide:

- A readable story around how the code has changed over time
- A mitigation against merge conflicts and rework
- An easy process for troubleshooting and repairing ([bisect](https://git-scm.com/docs/git-bisect),
  [revert](https://git-scm.com/docs/git-revert))


## LFS [^](#)

Git LFS is an extra system used to store large, typically non-text files in a source repository. Because of the
way that git checks for changes, these files can cause issues in the size and performance of git.

Good candidates for git LFS are:
- Audio/Video Files
- CAD/3D data files
- Large image files

For the DWD UI, we don't currently see a need for the extra configuration and management required for git LFS.
Any icon files, typically sized image files, etc, can be added to the repo directly without significant
overhead.

---
***⚠ Note:** If larger files are required, this may change.*

---
