# SO73653030

After login

- `/Home/Index` should redirect to `/accounts/ask-your-manager` because it has `[Authorize(Roles = "user1")]`
- `/Home/Privary` should redirect to `/subscriptions/upgrade-to-access` because it has `[Authorize(Policy = "AdvancedEdition")]`
