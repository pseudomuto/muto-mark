'use strict'

const fs = require('fs')
const md = require('app/markdown')

describe('Markdown', () => {
  describe('#toHTML', () => {
    let result = null

    context('with default options', () => {
      before(() => {
        let raw = fs.readFileSync('spec/fixtures/github.md').toString()
        return md.toHTML(raw).then(html => {
          result = html
        })
      })

      it('escapes underscores within words', () => {
        expect(result).to.contain('perform_complicated_task or do_this_and_do_that_and_another_thing')
      })

      it('escapes underscores in pre tags', () => {
        expect(result).to.contain('<span class="hljs-title">robot_invasion</span>')
      })

      it('treats individual new lines as whitespace', () => {
        expect(result).to.contain('<p>Roses are red\nViolets are blue</p>')
      })

      it('treats lines followed by 2 spaces as line breaks', () => {
        expect(result).to.contain('<p>Roses are red<br>Violets are blue</p>')
      })

      it('autolinks commit hashes', () => {
        expect(result).to.contain(
          '<a href="/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2" class="commit-link"><tt>be6a8cc</tt></a>'
        )
      })

      it('autolinks issue references', () => {
        expect(result).to.contain('<a href="/issues/1" class="title-link">mojombo/god#1</a>')
      })
    })

    context('when gfm turned off', () => {
      before(() => {
        let raw = fs.readFileSync('spec/fixtures/github.md').toString()
        return md.toHTML(raw, { gfm: false, tables: false }).then(html => {
          result = html
        })
      })

      it("doesn't apply github thingies", () => {
        expect(result).to.not.contain(
          '<a href="/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2" class="commit-link"><tt>be6a8cc</tt></a>'
        )

        expect(result).to.not.contain('<a href="/issues/1" class="title-link">mojombo/god#1</a>')
      })
    })
  })
})
