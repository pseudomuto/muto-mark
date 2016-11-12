'use strict'

import { hubify } from './github'
import marked from 'marked'
import hljs from 'highlight.js'

const defaults = {
  gfm: true,
  tables: true,
  breaks: false,
  highlight: (code, lang, callback) => {
    callback(null, hljs.highlightAuto(code).value)
  }
}

export const toHTML = (markdown, options) => {
  const settings = Object.assign(defaults, options || {})

  return new Promise((resolve, reject) => {
    try {
      const processedMarkdown = settings.gfm ? hubify(markdown) : markdown
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
