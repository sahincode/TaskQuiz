const btnsDelete = document.querySelectorAll(".btn-delete-slide");

btnsDelete.forEach(btn =>

    btn.addEventListener("click", function (e) {
        let href = btn.getAttribute("href")
        e.preventDefault()

        console.log("salam");
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(href).then(response => {
                    if (response.status == 404) {
                        Swal.fire({
                            title: "Does Not found!",
                            text: "Your file has not been deleted.",
                            icon: "error"
                        });

                    }
                    if (response.status == 200) {
                        window.location.reload(true)
                        Swal.fire({
                            title: "Deleted",
                            text: "Your file has  been deleted.",
                            icon: "success"
                        });


                    }
                })

            }
        });
    })
)