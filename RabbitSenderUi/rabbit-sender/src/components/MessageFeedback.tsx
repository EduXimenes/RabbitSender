type Props = {
  message: string;
};

export default function MessageFeedback({ message }: Props) {
  return (
    <div className="mt-4 text-center text-lg text-gray-700 font-medium">
      {message}
    </div>
  );
}
