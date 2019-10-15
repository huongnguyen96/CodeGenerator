export function redirect(url: string) {
  window.location.href = `${window.location.origin}${url}`;
}
