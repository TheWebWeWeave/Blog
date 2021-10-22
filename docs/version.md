# [Version](/README.md)
We are using gitVersion for all our products and packages to help us manage the version number which results in the build names.  For most of our applications it is pretty clear when we are adding new functionality, fixing a bug or introducing breaking changes.  With the Blog it is a little bit different because we don't add any functionality to the actual code of this site.  Here we use the version number to indicate if we are introducing new content or not.  

The following table will be the guide for what should be in the comment of the first commit of the following actions.

| Action | Commit Message |
|--------|----------------|
|New Blog Post|+semver: feature|
|Fixing a Blog no new content|+semver: hotfix |
|Change the Style or Engine| +semver: breaking |
