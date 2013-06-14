---
layout: default
title: MutoMark!
tagline: A Markdown Viewer for Windows
---
{% include JB/setup %}

<div class="hero-unit">
  <h1>{{ site.title }}</h1>
  <p>{{ site.tagline }}</p>
  <a class="btn btn-primary" href="{{ site.installer_url }}">Download Now!</a>
</div>

<div class="row">

  <span class="span4">
    <h2>Features</h2>
    <ul>
      <li>Live Preview whenever the source file changes</li>
      <li>Quickly view the generated HTML</li>
      <li>Complete Windows Installer file with uninstaller</li>
    </ul>
    <a href="help/installation.html" class="btn btn-success">Install Now!</a>
  </span>

  <span class="span4">
    <h2>Motivation</h2>
    <p>
      As a developer, I have really grown attached to my favorite editor (Sublime Text 2), so I wanted to find 
      a Markdown viewer that allowed me to use my edtior of choice...but I couldn't.
    </p>
    <p>
      If you are on a MAC, <a href-"http://markedapp.com/">Marked</a> is an amazing tool. My goal was to make a similar experience (hopefully comparable) 
      for Windows users (like me at work).
    </p>
  </span>
  <span class="span4">
    <h2>Code</h2>
    <p>I want to make this product great, which means I need your help!</p>
    <ul>
      <li>Fork this project on <a href="http://github.com/davidmuto/muto-mark">GitHub</a>
    </ul>
  </span>

</div>

<!-- Here's a sample "posts list".

<ul class="posts">
  {% for post in site.posts %}
    <li><span>{{ post.date | date_to_string }}</span> &raquo; <a href="{{ BASE_PATH }}{{ post.url }}">{{ post.title }}</a></li>
  {% endfor %}
</ul> -->