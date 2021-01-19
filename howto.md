# Bicom Systems Hackathon - How to use Github and git
![](https://hackathon.bicomsystems.com/wp-content/uploads/2019/08/Web02-1.png)
## What is GitHub?

GitHub is a code hosting platform for version control and collaboration. It lets you and others work together on projects from anywhere.

This tutorial teaches you GitHub essentials like  _repositories_,  _branches_,  _commits_, and  _Pull Requests_. You’ll create your own Hello World repository and learn GitHub’s Pull Request workflow, a popular way to create and review code.

## Create a Branch

**Branching**  is the way to work on different versions of a repository at one time.

By default your repository has one branch named  `main`  which is considered to be the definitive branch. We use branches to experiment and make edits before committing them to  `main`.

When you create a branch off the  `main`  branch, you’re making a copy, or snapshot, of  `main`  as it was at that point in time. If someone else made changes to the  `main`  branch while you were working on your branch, you could pull in those updates.

This diagram shows:

-   The  `main`  branch
-   A new branch called  `feature`  (because we’re doing ‘feature work’ on this branch)
-   The journey that  `feature`  takes before it’s merged into  `main`

![](https://guides.github.com/activities/hello-world/branching.png)

Have you ever saved different versions of a file? Something like:

-   `story.txt`
-   `story-joe-edit.txt`
-   `story-joe-edit-reviewed.txt`

Branches accomplish similar goals in GitHub repositories.

Here at GitHub, our developers, writers, and designers use branches for keeping bug fixes and feature work separate from our  `main`  (production) branch. When a change is ready, they merge their branch into  `main`

## Make and commit changes

Now, you’re on the code view for your  `readme-edits`  branch, which is a copy of  `main`. Let’s make some edits.

On GitHub, saved changes are called  _commits_. Each commit has an associated  _commit message_, which is a description explaining why a particular change was made. Commit messages capture the history of your changes, so other contributors can understand what you’ve done and why.

### Make and commit changes 
#### This shows how to commit changes from Github web application

1.  Click the  `README.md`  file.
2.  Click the  pencil icon in the upper right corner of the file view to edit.
3.  In the editor, write a bit about yourself.
4.  Write a commit message that describes your changes.
5.  Click  **Commit changes**  button.

 #### This shows how to do it from Terminal
1. Clone the empty project on your machine
 ``` git clone ``` 
 2. Make some changes 
 3. Type ```git add .``` 
	   > This command updates the index using the current content found in the working tree, to prepare the content staged for the next commit.
4. **Commit changes**
		``` git commit -m "information about your commit" ``` 
	> please be very descriptive when adding commits, to see why messages matter please refer to this [link](https://chris.beams.io/posts/git-commit/)
5.	 Push to your branch 
``` git push origin [name of your branch] ```
ex. ``` git push origin design-changes ``` be aware before doing this you actually have to switch to a working branch 
To switch to a working branch you can do ``` checkout [name of the branch] ```


## Open a Pull Request

Nice edits! Now that you have changes in a branch off of  `main`, you can open a  _pull request_.

Pull Requests are the heart of collaboration on GitHub. When you open a  _pull request_, you’re proposing your changes and requesting that someone review and pull in your contribution and merge them into their branch. Pull requests show  _diffs_, or differences, of the content from both branches. The changes, additions, and subtractions are shown in green and red.

As soon as you make a commit, you can open a pull request and start a discussion, even before the code is finished.

By using GitHub’s  [@mention system](https://help.github.com/articles/about-writing-and-formatting-on-github/#text-formatting-toolbar)  in your pull request message, you can ask for feedback from specific people or teams, whether they’re down the hall or 10 time zones away.

You can even open pull requests in your own repository and merge them yourself. It’s a great way to learn the GitHub flow before working on larger projects.

## Merge your Pull Request

In this final step, it’s time to bring your changes together – merging your  `readme-edits`  branch into the  `main`  branch.

1.  Click the green  **Merge pull request**  button to merge the changes into  `main`.
2.  Click  **Confirm merge**.
3.  Go ahead and delete the branch, since its changes have been incorporated, with the  **Delete branch**  button in the purple box.


### 😡 I have merge error, nooo?!

If you happen to get a merge error few things might went wrong. In most cases merging error can be fixed from terminal but it's not recommended for total beginners. To solve these errors you might want to use software such as VS Code which has amazing version control support.  Have look at ignore and accept changes. 

Be aware that you also have to pull new code version before starting new work on it.  So you'll have to use ``` git pull [name of the branch] ``` 

### 🤔 Helpful links: 


-  [Git for Absolute Beginners- FreeCodeCamp](https://www.freecodecamp.org/news/an-introduction-to-git-for-absolute-beginners-86fa1d32ff71/)
- [Git & Github Crash Course](https://www.youtube.com/watch?v=SWYqp7iY_Tc)
- [Git for Absolute Beginners](https://www.elegantthemes.com/blog/resources/git-and-github-a-beginners-guide-for-complete-newbies)

### Celebrate!

This document goes over these points
-   Created an open source repository
-   Started and managed a new branch
-   Changed a file and committed those changes to GitHub
-   Opened and merged a Pull Request

Take a look at your GitHub profile and you’ll see your new  [contribution squares](https://help.github.com/articles/viewing-contributions)!

To learn more about the power of Pull Requests, we recommend reading the  [GitHub flow Guide](http://guides.github.com/overviews/flow/). You might also visit  [GitHub Explore](http://github.com/explore)  and get involved in an Open Source project.

This document is forked from Github official guides and extended, if you need more information or you wish to see original file  you can visit [guides.github.com](https://guides.github.com/activities/hello-world/)



