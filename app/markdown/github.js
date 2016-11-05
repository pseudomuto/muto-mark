'use strict'

const COMMIT_LINKS = /\b([\w\/]+@)?([\w\d]{7})([\w\d]{33})\b/g
const ISSUE_LINKS = /(\b|^)?([\w\/]+)?#(\d+)\b/g

const fixCommitLinks = (md) => {
  return md.replace(COMMIT_LINKS, '<a href="/commit/$2$3" class="commit-link">$1<tt>$2</tt></a>')
}

const fixIssueLinks = (md) => {
  return md.replace(ISSUE_LINKS, '<a href="/issues/$3" class="title-link">$2#$3</a>')
}

exports.hubify = (input) => {
  return fixCommitLinks(fixIssueLinks(input))
}
