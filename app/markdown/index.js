'use strict'

const gh = require('./github')
const marked = require('marked')

const defaults = {
  gfm: true,
  tables: true,
  breaks: false
}

const toHTML = (markdown, options) => {
  let settings = Object.assign(defaults, options || {})

  return new Promise((resolve, reject) => {
    try {
      let processedMarkdown = settings.gfm ? gh.hubify(markdown) : markdown
      resolve(marked(processedMarkdown, settings))
    } catch (error) {
      reject(error)
    }
  })
}

exports.toHTML = toHTML
