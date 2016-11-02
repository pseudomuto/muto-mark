import { hubify } from './github'
import marked from 'marked'

const defaults = {
  gfm: true,
  tables: true,
  breaks: false
}

export const toHTML = (markdown, options) => {
  let settings = Object.assign(defaults, options || {})

  return new Promise((resolve, reject) => {
    try {
      let processedMarkdown = settings.gfm ? hubify(markdown) : markdown
      resolve(marked(processedMarkdown, settings))
    } catch (error) {
      reject(error)
    }
  })
}
