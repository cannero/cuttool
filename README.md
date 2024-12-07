# cuttool
Program your own `cut` https://codingchallenges.fyi/challenges/challenge-cut/

## using jj
### general
- after adding file to .gitignore remove it from tracking  
`jj file untrack <file>`
- add a remote  
`jj git remote add origin https://github.com/cannero/cuttool.git`
- create a branch of previous commit  
`jj bookmark create main -r @-`
- update bookmark to @  
`jj bookmark set main`
- push new bookmark  
`jj git push --allow-new`
- show all commits  
`jj log -r 'all()'`

