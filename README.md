# TryTLS

Does *your* library check TLS certificates properly?
Broken certificate checks seems to be an overlooked issue.
Handling certificates is surprisingly complex, and calls for extra attention.

TryTLS is a tool for the software and library developers, vulnerability
researchers, and end-users, who want to use TLS safely.

We hope to help you to test certificate handling easily. We support
systematic and readily planned tests and try make integrating your
favorite language and library easy.

## How Does It Work?

![Architecture](doc/concept-pic.png)

 * **Backends** use ports and virtual hosts to provide falsified/broken certificate checks
 * **Stubs** are written for the target languages and libraries to attempt the TLS connection
 * **Runners** "check the checks" by calling the stubs systematically to find out
 how libraries handle signatures, domain names, time, SNI etc. against the backends

## Runners

We have a [Python based test runner](showrunner/) and a work-in-progress
[bash based test runner](runners/bashtls/data/shared/simplerunner).

### Installation

```sh
$ git clone https://github.com/ouspg/trytls.git
$ cd trytls
$ pip install .
```

In case you don't have [`pip`](https://pip.pypa.io/) installed, please refer to [these instructions](http://docs.python-guide.org/en/latest/starting/installation/).

### Usage

```sh
$ trytls https python stubs/python-urllib2/run.py
  PASS badssl(False, 'expired')
  PASS badssl(False, 'wrong.host')
  PASS badssl(False, 'self-signed')
  PASS badssl(True, 'sha256')
  PASS badssl(True, '1000-sans')
x FAIL badssl(True, '10000-sans')
x FAIL badssl(False, 'incomplete-chain')
  PASS badssl(False, 'superfish')
  PASS badssl(False, 'edellroot')
  PASS badssl(False, 'dsdtestprovider')
x FAIL badssl_onlymyca(False, 'sha256')
  PASS local(True, 'localhost', callback=<function https_callback at 0x108efe050>)
  PASS local(False, 'nothing', callback=<function https_callback at 0x108efe050>)
```

## Stubs

Stubs and their documentation can be found from the [stubs/](stubs/) directory.

## Backends

We currently are working to support following backends:

 * [BadSSL](https://badssl.com) - we have cherry picked the [relevant tests](backends/badssl/README.md)
 * Local backend in the test runner itself (aka `localhost` backend) [WIP]
 * [Trytls backend](backends/trytls) both as docker based "run-it-yourself" packaging and as a
 hosted service provided by us [WIP]

Test runners allow user to test against all or any of these backends.

## What TryTLS Is Not

 * We do not address possible client certificate check problems in server code
 * We do not do or require a man-in-the-middle tools
 * We do not support smart TVs, IoT toasters and other such devices that can't run the test driver

## Found issues

  * [Wreq connection to HTTPS site with invalid hostname · Issue #84 · bos/wreq GitHub](https://github.com/bos/wreq/issues/84)
   * See also [Is Wreq suitable for HTTPS applications? · Issue #82 · bos/wreq · GitHub](https://github.com/bos/wreq/issues/82)
   * Related [http-client-tls connection to HTTPS site with invalid hostname · Issue #212 · snoyberg/http-client · GitHub](https://github.com/snoyberg/http-client/issues/212)

## Contributors

 * Mauri Miettinen ([@Mamietti](https://github.com/Mamietti))
 * Aleksi Klasila ([@aleksiklasila](https://github.com/aleksiklasila))
 * ...

We invite people to [contribute](CONTRIBUTING.md).
