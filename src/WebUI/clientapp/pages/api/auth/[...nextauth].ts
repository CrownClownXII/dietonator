import NextAuth from "next-auth"
import IdentityServer4Provider from 'next-auth/providers/identity-server4'

export const authOptions = {
  // Configure one or more authentication providers
  providers: [
    IdentityServer4Provider({
        id: "demo-identity-server",
        name: "Demo IdentityServer",
        authorization: { params: { scope: "openid profile DietonatorAPI" } },
        issuer:  "https://localhost:7001/",
        clientId: "nextjs_web_app",
        clientSecret: "secret",
    })
  ],
}
export default NextAuth(authOptions)