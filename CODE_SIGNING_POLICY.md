# Code signing policy

Free code signing provided by SignPath.io, certificate by SignPath Foundation.

## Status

Applying to the SignPath Foundation program. Until approval, release artifacts are
published unsigned, and Windows SmartScreen may show a warning. Users can verify the
file with the SHA-256 checksum published in the release notes.

## What will be signed

- The Windows installer produced from this repository (`RiaLauncher-<version>-Setup.exe`,
  built with Inno Setup by the GitHub Actions workflow in `.github/workflows/build.yml`),
  published on GitHub Releases and distributed via Softonic.

## Build and signing process

- Every artifact is built automatically from this repository by GitHub Actions.
- Only CI-built artifacts are submitted to SignPath for signing; nothing is built or
  signed manually on a developer machine.
- The private key never leaves SignPath (HSM-backed). This project does not store any
  private key material.
- Each release requires an explicit approval before signing, and source code review
  includes the workflow file and the installer script (`setup/RiaLauncher.iss`).

## Team roles

Single-maintainer project (Riasoft).

- Authors (commit access): https://github.com/hikmetalemdaroglu
- Reviewers: all pull requests from non-committers are reviewed by the maintainer
  before merge.
- Approvers: every signing request is approved by the maintainer.

## Verification for users

- Releases are published only on https://github.com/Riasoftapp/RiaLauncher/releases
- Every release lists the SHA-256 checksum of the installer.
- After signing is enabled, the installer will carry an Authenticode signature issued
  to SignPath Foundation.
