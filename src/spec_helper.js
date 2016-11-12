import chai from 'chai'
import chaiAsPromised from 'chai-as-promised'
import sinon from 'sinon'

chai.config.includeStack = true
chai.use(chaiAsPromised)

global.assert = chai.assert
global.expect = chai.expect
global.should = chai.should()
global.sinon = sinon
