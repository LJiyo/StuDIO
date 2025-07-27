export async function getStudents() {
  const response = await fetch('/api/students');
  return response.json();
}
