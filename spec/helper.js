const chai = require('chai')
const chaiAsPromised = require('chai-as-promised')
const sinon = require('sinon')

chai.config.includeStack = true
chai.use(chaiAsPromised)

global.assert = chai.assert
global.expect = chai.expect
global.should = chai.should()
global.sinon = sinon
