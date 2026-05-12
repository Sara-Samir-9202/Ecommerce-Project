export 
interface TokenRes {
  message: string;
  decoded: Decoded;
}

interface Decoded {
  id: string;
  name: string;
  role: string;
  iat: number;
  exp: number;
}