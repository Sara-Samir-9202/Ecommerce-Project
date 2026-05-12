export
interface PaymentRes {
  status: string;
  session: Session;
}

interface Session {
  url: string;
  success_url: string;
  cancel_url: string;
}