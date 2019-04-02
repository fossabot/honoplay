const Style = (theme) => ({
    card: {
        maxWidth: 250,
        backgroundColor: '#444444',     
      },
      icon: {
        color: 'white',
        fontSize: 40,
      },
      center: {
        display: 'flex',
        justifyContent: 'center',
        paddingTop: 10,
      },
      typography: {
        color: 'white',
        textAlign: 'center',
        margin: theme.spacing.unit,
        fontWeight: 'bold',
        fontSize:15
      },
      paragraph: {
        color: 'white',
        margin: theme.spacing.unit,
        textAlign: 'center',
        width: '100%',
        fontSize:13
      },
});

export default Style;