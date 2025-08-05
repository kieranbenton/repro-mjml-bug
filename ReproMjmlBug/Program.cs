using Mjml.Net;

// NOTE: looks like ALL THREE of the previous runs need to execute in order to reproduce the bug

// ok
string one = "<mjml>\n<mj-head>\n<mj-title>Hello World Example</mj-title>\n</mj-head>\n<mj-body>\n<mj-section>\n<mj-column>\n<mj-text>\nHello World! <span data-content-type=\"textunit\" data-content-key=\"test_key\" data-content-channelid=\"chanl_00000000-0000-0000-0000-000000000002\" data-content-language=\"en-GB\" data-content-is-draft=\"false\" data-content-limit=\"0\" data-content-is-inline=\"true\"></span> [[TRANSLATED!: 'test_key' in 'en-GB']]\n</mj-text>\n</mj-column>\n</mj-section>\n</mj-body>\n</mjml>";
Render(one);

// ok 
string two = "<mjml>\n<mj-head>\n<mj-title>Hello World Example</mj-title>\n</mj-head>\n<mj-body>\n<mj-section>\n<mj-column>\n<mj-text>\nHello World! <span data-content-type=\"textunit\" data-content-key=\"test_key\" data-content-channelid=\"chanl_00000000-0000-0000-0000-000000000002\" data-content-language=\"en-GB\" data-content-is-draft=\"false\" data-content-limit=\"0\" data-content-is-inline=\"true\"></span> [[TRANSLATED!: 'test_key' in 'en-GB']]\n</mj-text>\n</mj-column>\n</mj-section>\n</mj-body>\n</mjml><mjml>\n<mj-head>\n<mj-title>Hello World Example</mj-title>\n</mj-head>\n<mj-body>\n<mj-section>\n<mj-column>\n<mj-text>\nHello World!  [[TRANSLATED!: 'test_key' in 'en-GB']]\n</mj-text>\n</mj-column>\n</mj-section>\n</mj-body>\n</mjml>";
Render(two);

// bad - fails
string three = "<mjml>\n<mj-head>\n<mj-title>Hello World Example</mj-title>\n</mj-head>\n<mj-body>\n<mj-section>\n<mj-column>\n<mj-text>\nHello World! <span data-content-type=\"textunit\" data-content-key=\"test_key\" data-content-channelid=\"chanl_00000000-0000-0000-0000-000000000002\" data-content-language=\"en-GB\" data-content-is-draft=\"false\" data-content-limit=\"0\" data-content-is-inline=\"true\"></span> [[TRANSLATED!: 'test_key' in 'en-GB']]\n</mj-text>\n</mj-column>\n</mj-section>\n</mj-body>\n</mjml>\nthis is some new content";
Render(three);

// BAD - this is the one that shouldn't have "data-content-key" in it but it does
string four = "<mjml>\n<mj-head>\n<mj-title>Hello World Example</mj-title>\n</mj-head>\n<mj-body>\n<mj-section>\n<mj-column>\n<mj-text>\nHello World!  [[TRANSLATED!: 'test_key' in 'en-GB']]\n</mj-text>\n</mj-column>\n</mj-section>\n</mj-body>\n</mjml>\n<mj-text font-family=\"Helvetica\" color=\"#F45E43\"><p>this is some new content</p></mj-text>";
string fourResult = Render(four);
if (!four.Contains("data-content-key") && fourResult.Contains("data-content-key"))
{
    Console.WriteLine("THIS SHOULD BE IMPOSSIBLE - 'data-content-key' should not be in the output HTML");
}

//

static string Render(string firstBadMjml)
{
    var mjmlRenderer = new MjmlRenderer();

    var (html, errors) = mjmlRenderer.Render(firstBadMjml);

    if (errors.Any())
        return "";

    return html;
}