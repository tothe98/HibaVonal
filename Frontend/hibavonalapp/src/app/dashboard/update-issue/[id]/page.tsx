import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { decrypt, fetchCurrentUser } from "@/lib/session";
import UpdateIssueClientPage from "./page.client";

interface Params {
  id: string;
}

export default async function UpdateIssuePage({ params }: { params: Promise<Params> }) {
  const { id } = await params;

  const cookieStore = await cookies();
  const token = (await cookieStore.get("session"))?.value;

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

  return <UpdateIssueClientPage user={user} updateId={parseInt(id)} />;
}
