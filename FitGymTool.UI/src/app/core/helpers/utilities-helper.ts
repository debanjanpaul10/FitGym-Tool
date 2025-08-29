import { WritableSignal } from '@angular/core';
import { AIChatbotResponseDTO } from '@models/DTO/ai-chatbot-response-dto.model';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { ChatMessage } from '@models/interfaces/chat-message.interface';

/**
 * The utilities helper class.
 */
export class Utilities {
  /**
   * Converts a Date object to a standardized yyyy-MM-dd string format.
   * Ensures consistent date formatting for comparison operations in filters.
   * @param date - The Date object to format
   * @returns A string representation of the date in yyyy-MM-dd format
   */
  public static FormatDateToYMD(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  /**
   * Validates a member's name field.
   * @param name - The name string to validate
   * @returns True if name is between 2-100 characters, false otherwise
   */
  public static IsValidName(name: string): boolean {
    return name !== '' && name.trim().length >= 2 && name.trim().length <= 100;
  }

  /**
   * Validates a member's email address using regex pattern.
   * @param email - The email string to validate
   * @returns True if email matches valid email format, false otherwise
   */
  public static IsValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return email !== '' && emailRegex.test(email.trim());
  }

  /**
   * Validates a member's phone number format.
   * @param phone - The phone number string to validate
   * @returns True if phone number contains exactly 10 digits, false otherwise
   */
  public static IsValidPhoneNumber(phone: string): boolean {
    const phoneRegex = /^\d{10}$/;
    return phone !== '' && phoneRegex.test(phone.replace(/\s/g, ''));
  }

  /**
   * Validates a member's address field.
   * @param address - The address string to validate
   * @returns True if address is between 5-500 characters, false otherwise
   */
  public static IsValidAddress(address: string): boolean {
    return (
      address !== '' &&
      address.trim().length >= 5 &&
      address.trim().length <= 500
    );
  }

  /**
   * Validates a member's gender selection.
   * @param gender - The gender string to validate
   * @returns True if gender is one of the allowed values (Male, Female, Other), false otherwise
   */
  public static IsValidGender(gender: string): boolean {
    return gender !== '' && ['Male', 'Female', 'Other'].includes(gender);
  }

  /**
   * Validates a date object to ensure it's a valid Date instance.
   * @param date - The Date object to validate
   * @returns True if date is a valid Date object, false otherwise
   */
  public static IsValidDate(date: Date): boolean {
    return date && date instanceof Date && !isNaN(date.getTime());
  }

  /**
   * Validates whether the provided master mapping data contains valid and usable information.
   * Checks for the presence of membership status mappings and other array-based mapping data.
   * Returns true if valid data exists, false otherwise.
   */
  public static CheckValidMappingDataExists(
    data: MasterMappingDataDto
  ): boolean {
    return (
      data &&
      ((data.membershipStatusMapping &&
        data.membershipStatusMapping.length > 0) ||
        Object.keys(data).some((key) => {
          const value = (data as any)[key];
          return Array.isArray(value) && value.length > 0;
        }))
    );
  }

  /**
   * Gets the greeting based on time of the day.
   * @returns {string} The greeting
   */
  public static GetGreeting(): string {
    const hour = new Date().getHours();

    if (hour < 12) return 'Morning';
    if (hour < 17) return 'Afternoon';
    return 'Evening';
  }

  /**
   * Gets the CSS class for the status chip based on the status value
   */
  public static GetStatusChipClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'active':
        return 'status-chip status-chip-active';
      case 'disabled':
        return 'status-chip status-chip-inactive';
      case 'decommissioned':
        return 'status-chip status-chip-pending';
      default:
        return 'status-chip status-chip-default';
    }
  }

  /**
   * Parses the markdown content table.
   * @param markdownContent The markdown content.
   * @returns The tupple containing the header and its rows.
   */
  public static ParseMarkdownTable(
    markdownContent: string
  ): { headers: string[]; rows: string[][] } | null {
    const lines = markdownContent.trim().split('\n');
    const tableLines = lines.filter(
      (line) => line.trim().startsWith('|') && line.trim().endsWith('|')
    );

    if (tableLines.length < 3) return null;

    const headerLine = tableLines[0];
    const headers = headerLine
      .split('|')
      .slice(1, -1)
      .map((header) => header.trim());

    const dataLines = tableLines.slice(2);

    const rows = dataLines.map((line) =>
      line
        .split('|')
        .slice(1, -1)
        .map((cell) => Utilities.FormatTableCellValue(cell.trim()))
    );

    return { headers, rows };
  }

  /**
   * Detects if a string is a date and formats it properly
   * @param value The string value to check and format
   * @returns Formatted date string or original value if not a date
   */
  public static FormatTableCellValue(value: string): string {
    if (!value || value.trim() === '') return value;

    // Check if the value looks like a date (various formats)
    const datePatterns = [
      /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}/, // ISO format with time
      /^\d{4}-\d{2}-\d{2}$/, // YYYY-MM-DD
      /^\d{2}\/\d{2}\/\d{4}$/, // MM/DD/YYYY
      /^\d{2}-\d{2}-\d{4}$/, // MM-DD-YYYY
    ];

    const isDate = datePatterns.some((pattern) => pattern.test(value.trim()));

    if (isDate) {
      try {
        const date = new Date(value);
        if (this.IsValidDate(date)) {
          // Format as readable date
          return date.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'short',
            day: 'numeric',
          });
        }
      } catch (error) {
        // If parsing fails, return original value
        return value;
      }
    }

    return value;
  }

  /**
   * Prepares the chatbot greeting message.
   * @param currentUserName The current user name
   * @returns {string} The chatbot greeting message.
   */
  public static GetChatbotGreetingMessage(
    currentUserName: string | undefined
  ): string {
    currentUserName = currentUserName ?? 'there';
    return `Hello ${currentUserName}! I'm your AI assistant. How can I help you today?`;
  }

  /**
   * Converts markdown text to HTML for display
   * @param markdown The markdown text to convert
   * @returns HTML string
   */
  public static ParseMarkdownToHtml(markdown: string): string {
    if (!markdown) return '';

    let html = markdown;

    // Handle code blocks (```code```)
    html = html.replace(/```([\s\S]*?)```/g, '<pre><code>$1</code></pre>');

    // Handle inline code (`code`)
    html = html.replace(/`([^`]+)`/g, '<code>$1</code>');

    // Handle bold (**text** or __text__)
    html = html.replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>');
    html = html.replace(/__(.*?)__/g, '<strong>$1</strong>');

    // Handle italic (*text* or _text_)
    html = html.replace(/\*(.*?)\*/g, '<em>$1</em>');
    html = html.replace(/_(.*?)_/g, '<em>$1</em>');

    // Handle headers
    html = html.replace(/^### (.*$)/gm, '<h3>$1</h3>');
    html = html.replace(/^## (.*$)/gm, '<h2>$1</h2>');
    html = html.replace(/^# (.*$)/gm, '<h1>$1</h1>');

    // Handle links [text](url)
    html = html.replace(
      /\[([^\]]+)\]\(([^)]+)\)/g,
      '<a href="$2" target="_blank">$1</a>'
    );

    // Handle line breaks
    html = html.replace(/\n\n/g, '</p><p>');
    html = html.replace(/\n/g, '<br>');

    // Wrap in paragraph tags if not already wrapped
    if (!html.startsWith('<')) {
      html = '<p>' + html + '</p>';
    }

    // Handle unordered lists
    html = html.replace(/^\* (.+)$/gm, '<li>$1</li>');
    html = html.replace(/(<li>.*<\/li>)/s, '<ul>$1</ul>');

    // Handle ordered lists
    html = html.replace(/^\d+\. (.+)$/gm, '<li>$1</li>');

    return html;
  }
}
