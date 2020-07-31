#docker run --rm --volume "$(pwd):/repo" gittools/gitversion:5.3.4-linux-alpine.3.10-x64-netcoreapp3.1 /repo -output json
echo "We are here: ${pwd}"
echo "The correct version number: ${semver}"
echo "The actual branch is: ${branchname}"
echo "The git_branch is: ${env.GIT_BRANCH}"

git checkout "${branchname}