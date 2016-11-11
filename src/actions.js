'use strict'

import { readFile } from 'fs'
import { toHTML } from './markdown'

export const CONTENT_CHANGED = 'CONTENT_CHANGED'

const updateContents = (html) => {
  return { type: CONTENT_CHANGED, html }
}

const readFileAsync = (path) => {
  return new Promise((resolve, reject) => {
    readFile(path, (error, content) => {
      if (error) {
        reject(error)
      } else {
        resolve(content)
      }
    })
  })
}

export const convertFileToHTML = (path) => {
  return dispatch => {
    return readFileAsync(path).then(contents => {
      const md = contents.toString()
      return toHTML(md).then(html => dispatch(updateContents(html)))
    })
  }
}
