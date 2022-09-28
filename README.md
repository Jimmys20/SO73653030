# Understanding the difference between Authorized Claims and Authorized Roles

https://stackoverflow.com/questions/73653030/understanding-the-difference-between-authorized-claims-and-authorized-roles

After login

- `/Home/Index` should redirect to `/accounts/ask-your-manager` because it has `[Authorize(Roles = "user1")]`
- `/Home/Privary` should redirect to `/subscriptions/upgrade-to-access` because it has `[Authorize(Policy = "AdvancedEdition")]`
