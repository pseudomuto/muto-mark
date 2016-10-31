'use strict'

const github = require('../../lib/markdown/github')

describe('Github Markdown Processing', () => {
  context('#hubify', () => {
    describe('commit links', () => {
      context('when isolated', () => {
        it('converts to commit link', () => {
          let expected = '<a href="/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2" class="commit-link"><tt>be6a8cc</tt></a>'
          expect(github.hubify('be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2')).to.equal(expected)
        })
      })

      context('when prefixed with a mention', () => {
        it('converts to user-specific commit link', () => {
          let expected = '<a href="/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2" class="commit-link">mojombo@<tt>be6a8cc</tt></a>'
          expect(github.hubify('mojombo@be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2')).to.equal(expected)
        })
      })

      context('when prefixed with a mention and a project', () => {
        it('converts to user and project specific commit link', () => {
          let expected = '<a href="/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2" class="commit-link">mojombo/god@<tt>be6a8cc</tt></a>'
          expect(github.hubify('mojombo/god@be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2')).to.equal(expected)
        })
      })
    })

    describe('issue links', () => {
      context('when isolated', () => {
        it('converts to issue link', () => {
          let expected = '<a href="/issues/1" class="title-link">#1</a>'
          expect(github.hubify('#1')).to.equal(expected)
        })
      })

      context('when prefixed with a mention', () => {
        it('converts to user specific issue link', () => {
          let expected = '<a href="/issues/1" class="title-link">mojombo#1</a>'
          expect(github.hubify('mojombo#1')).to.equal(expected)
        })
      })

      context('when prefixed with a mention and a project', () => {
        it('converts to user and project specific issue link', () => {
          let expected = '<a href="/issues/1" class="title-link">mojombo/god#1</a>'
          expect(github.hubify('mojombo/god#1')).to.equal(expected)
        })
      })
    })
  })
})
