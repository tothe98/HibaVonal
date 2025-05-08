import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { decrypt, fetchCurrentUser } from "@/lib/session";
import MakeOrderClientPage from "./page.client";

export default async function MakeOrderPage() {
  const cookieStore = await cookies();
  const token = cookieStore.get("session")?.value;

  if (!token) {
    redirect("/");
  }

  const session = await decrypt(token);
  if (!session?.email) {
    redirect("/");
  }

  const user = await fetchCurrentUser(token);
  if (!user || !user.roles) {
    redirect("/");
  }

  return <MakeOrderClientPage user={user} />;
}
