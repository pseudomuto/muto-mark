'use strict'

const gh = require('./github')
const marked = require('marked')
const hljs = require('highlight.js')

const defaults = {
  gfm: true,
  tables: true,
  breaks: false,
  highlight: (code, lang, callback) => {
    callback(null, hljs.highlightAuto(code).value)
  }
}

const toHTML = (markdown, options) => {
  let settings = Object.assign(defaults, options || {})

  return new Promise((resolve, reject) => {
    try {
      let processedMarkdown = settings.gfm ? gh.hubify(markdown) : markdown
      marked(processedMarkdown, settings, (err, content) => {
        if (err) {
          reject(err)
        } else {
          resolve(content)
        }
      })
    } catch (error) {
      reject(error)
    }
  })
}

exports.toHTML = toHTML
