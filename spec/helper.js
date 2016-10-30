const chai = require('chai')

chai.config.includeStack = true

global.assert = chai.assert
global.expect = chai.expect
global.should = chai.should()
